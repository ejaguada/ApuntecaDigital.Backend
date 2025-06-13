import { useAuthStore } from '~/stores/auth'

export default defineNuxtRouteMiddleware(to => {
  const authStore = useAuthStore()
  const requiredRole = to.meta.requiresRole as string

  if (!authStore.isLoggedIn)
    return navigateTo('/login')

  if (requiredRole && !authStore.hasRole(requiredRole)) {
    throw createError({
      statusCode: 403,
      statusMessage: 'Access Denied',
    })
  }
})
