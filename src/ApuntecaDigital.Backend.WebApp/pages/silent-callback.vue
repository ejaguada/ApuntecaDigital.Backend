<script setup lang="ts">
onMounted(async () => {
  const authStore = useAuthStore()

  try {
    if (authStore.userManager)
      await authStore.userManager.signinSilentCallback()
  }
  catch (error) {
    console.error('Silent callback error:', error)
  }

  // Close the iframe/popup
  if (window.parent !== window)
    window.close()
})
</script>

<template>
  <div>Processing...</div>
</template>
