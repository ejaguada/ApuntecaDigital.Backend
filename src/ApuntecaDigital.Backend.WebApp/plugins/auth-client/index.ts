import { useAuthStore } from '~/stores/auth' // Adjust the path as needed

export default defineNuxtPlugin(async () => {
  const authStore = useAuthStore()

  // Initialize user manager
  await authStore.initializeUserManager()

  // Load existing user session
  await authStore.loadUser()
})
