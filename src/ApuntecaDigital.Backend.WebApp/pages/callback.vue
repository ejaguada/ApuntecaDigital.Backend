<script setup lang="ts">
const authStore = useAuthStore()
const router = useRouter()

const isLoading = ref(true)
const error = ref('')

onMounted(async () => {
  try {
    const returnUrl = await authStore.handleCallback()

    await router.push(returnUrl ?? '/')
  }
  catch (err: any) {
    console.error('Authentication callback error:', err)
    error.value = err.message || 'Authentication failed. Please try again.'
  }
  finally {
    isLoading.value = false
  }
})
</script>

<template>
  <div class="min-h-screen flex items-center justify-center">
    <div class="text-center">
      <div
        v-if="isLoading"
        class="animate-spin rounded-full h-32 w-32 border-b-2 border-gray-900 mx-auto"
      />
      <div
        v-else-if="error"
        class="text-red-600"
      >
        <h2 class="text-xl font-semibold mb-2">
          Authentication Error
        </h2>
        <p>{{ error }}</p>
        <button
          class="mt-4 px-4 py-2 bg-blue-500 text-white rounded"
          @click="$router.push('/')"
        >
          Go Home
        </button>
      </div>
    </div>
  </div>
</template>
