import { useAuthStore } from '~/stores/auth'

export default defineNuxtRouteMiddleware(to => {
  const authStore = useAuthStore()

  if (!authStore.isLoggedIn) {
    // Trigger login with current URL as return URL
    authStore.login(to.fullPath)
  }
})
