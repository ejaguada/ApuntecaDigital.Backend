<script setup lang="ts">
import auth from '@/middleware/auth'
import { useAuthStore } from '@/stores/auth'

definePageMeta({
  middleware: auth,
})

const authStore = useAuthStore()

const logout = async () => {
  await authStore.logout()
}
</script>

<template>
  <div>
    <nav class="bg-gray-800 text-white p-4">
      <div class="flex justify-between items-center">
        <h1>Dashboard</h1>
        <div class="flex items-center space-x-4">
          <span>Welcome, {{ authStore.userProfile?.name }}</span>
          <button
            class="bg-red-600 px-3 py-1 rounded"
            @click="logout"
          >
            Logout
          </button>
        </div>
      </div>
    </nav>

    <div class="p-6">
      <h2>User Information</h2>
      <pre>{{ JSON.stringify(authStore.userProfile, null, 2) }}</pre>

      <div
        v-if="authStore.hasRole('admin')"
        class="mt-4"
      >
        <h3>Admin Section</h3>
        <p>This content is only visible to admins.</p>
      </div>
    </div>
  </div>
</template>
