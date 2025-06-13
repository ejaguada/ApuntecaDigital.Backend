import type { User } from 'oidc-client-ts'
import { UserManager, WebStorageStateStore } from 'oidc-client-ts'
import { defineStore } from 'pinia'

interface AuthState {
  user: User | null
  isAuthenticated: boolean
  isLoading: boolean
  userManager: UserManager | null
}

export const useAuthStore = defineStore('auth', {
  state: (): AuthState => ({
    user: null,
    isAuthenticated: false,
    isLoading: false,
    userManager: null,
  }),

  getters: {
    isLoggedIn: state => state.isAuthenticated && !!state.user,
    userProfile: state => state.user?.profile,
    accessToken: state => state.user?.access_token,
    hasRole: state => (role: string) => {
      const roles = state.user?.profile?.role
      if (Array.isArray(roles))
        return roles.includes(role)

      return roles === role
    },
    hasPermission: state => (permission: string) => {
      const permissions = state.user?.profile?.permissions
      if (Array.isArray(permissions))
        return permissions.includes(permission)

      return permissions === permission
    },
  },

  actions: {
    async initializeUserManager() {
      if (process.client) {
        const config = useRuntimeConfig()

        this.userManager = new UserManager({
          authority: config.public.oidc.authority as string,
          client_id: config.public.oidc.client_id as string,
          redirect_uri: config.public.oidc.redirect_uri as string,
          post_logout_redirect_uri: config.public.oidc.post_logout_redirect_uri as string,
          response_type: 'code',
          scope: config.public.oidc.scope as string,
          automaticSilentRenew: true,
          silent_redirect_uri: (config.public.oidc.redirect_uri as string).replace('/callback', '/silent-callback'),
          userStore: new WebStorageStateStore({ store: window.localStorage }),
        })

        // Handle token expiration
        this.userManager.events.addAccessTokenExpired(() => {
          this.clearUser()
        })

        this.userManager.events.addUserSignedOut(() => {
          this.clearUser()
        })
      }
    },
    async handleCallback() {
      if (!this.userManager)
        return

      try {
        const user = await this.userManager.signinRedirectCallback()

        this.user = user
        this.isAuthenticated = true

        // Get stored return URL
        const returnUrl = sessionStorage.getItem('auth_return_url') || '/dashboard'

        sessionStorage.removeItem('auth_return_url')

        return returnUrl
      }
      catch (error) {
        console.error('Signin callback error:', error)
        throw error
      }
    },
    async loadUser() {
      if (!this.userManager)
        return

      this.isLoading = true
      try {
        const user = await this.userManager.getUser()
        if (user && !user.expired) {
          this.user = user
          this.isAuthenticated = true
        }
        else {
          this.user = null
          this.isAuthenticated = false
        }
      }
      catch (error) {
        console.error('Error loading user:', error)
        this.user = null
        this.isAuthenticated = false
      }
      finally {
        this.isLoading = false
      }
    },

    async signinRedirect() {
      if (!this.userManager)
        return
      await this.userManager.signinRedirect()
    },

    async signinRedirectCallback() {
      if (!this.userManager)
        return

      try {
        const user = await this.userManager.signinRedirectCallback()

        this.user = user
        this.isAuthenticated = true

        return user
      }
      catch (error) {
        console.error('Signin callback error:', error)
        throw error
      }
    },

    async signoutRedirect() {
      if (!this.userManager)
        return

      await this.userManager.signoutRedirect()
    },

    async renewToken() {
      if (!this.userManager)
        return

      try {
        const user = await this.userManager.signinSilent()

        this.user = user
        this.isAuthenticated = true

        return user
      }
      catch (error) {
        console.error('Token renewal failed:', error)
        await this.signoutRedirect()
      }
    },

    async login(returnUrl?: string) {
      if (!this.userManager)
        return

      // Store return URL for after login
      if (returnUrl)
        sessionStorage.setItem('auth_return_url', returnUrl)

      await this.userManager.signinRedirect()
    },
    async logout() {
      if (!this.userManager)
        return

      this.clearUser()
      await this.userManager.signoutRedirect()
    },

    clearUser() {
      this.user = null
      this.isAuthenticated = false
    },
    async renewTokenSilently() {
      if (!this.userManager)
        return false

      try {
        const user = await this.userManager.signinSilent()

        this.user = user
        this.isAuthenticated = true

        return true
      }
      catch (error) {
        console.error('Silent renewal failed:', error)
        this.clearUser()

        return false
      }
    },
  },
})
