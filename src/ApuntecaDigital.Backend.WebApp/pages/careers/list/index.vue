<script lang="ts" setup>
import type { ECareerData } from '../../../types/careers/types'
import auth from '@/middleware/auth'

const headers = [
  { title: 'Name', key: 'name' },
]

// const resolveCategory = (category: string) => {
//   if (category === 'Accessories')
//     return { color: 'error', icon: 'tabler-device-watch' }
//   if (category === 'Home Decor')
//     return { color: 'info', icon: 'tabler-home' }
//   if (category === 'Electronics')
//     return { color: 'primary', icon: 'tabler-device-imac' }
//   if (category === 'Shoes')
//     return { color: 'success', icon: 'tabler-shoe' }
//   if (category === 'Office')
//     return { color: 'warning', icon: 'tabler-briefcase' }
//   if (category === 'Games')
//     return { color: 'primary', icon: 'tabler-device-gamepad-2' }
// }

// const resolveStatus = (statusMsg: string) => {
//   if (statusMsg === 'Scheduled')
//     return { text: 'Scheduled', color: 'warning' }
//   if (statusMsg === 'Published')
//     return { text: 'Publish', color: 'success' }
//   if (statusMsg === 'Inactive')
//     return { text: 'Inactive', color: 'error' }
// }
const searchQuery = ref('')
const careersData = ref({ careers: [], total: 0 })

const careers = computed((): ECareerData[] => careersData.value.careers)
const totalCareers = computed(() => careersData.value.total)

const fetchCareers = async () => {
  // Fetch careers data from the API
  const { data } = await useApi<any>(createUrl('/Careers', {
    query: {
      name: searchQuery.value,
    },
  }))

  careersData.value = data.value
}

onMounted(() => {
  fetchCareers()
})

watch(searchQuery, () => {
  // Refetch careers when search query changes
  fetchCareers()
})

// const deleteCareer = async (id: number) => {
//   await $api(`apps/ecommerce/careers/${id}`, {
//     method: 'DELETE',
//   })

//   // Delete from selectedRows
//   const index = selectedRows.value.findIndex(row => row === id)
//   if (index !== -1)
//     selectedRows.value.splice(index, 1)

//   // Refetch careers
//   fetchCareers()
// }

definePageMeta({
  middleware: auth,
})
</script>

<template>
  <div>
    <!-- 👉 careers -->
    <VCard
      title="Filters"
      class="mb-6"
    >
      <VCardText>
        <VRow>
          <!-- 👉 Select Status -->
          <!--
            <VCol
            cols="12"
            sm="4"
            >
            <AppSelect
            v-model="selectedStatus"
            placeholder="Status"
            :items="status"
            clearable
            clear-icon="tabler-x"
            />
            </VCol>
          -->

          <!-- 👉 Select Category -->
          <!--
            <VCol
            cols="12"
            sm="4"
            >
            <AppSelect
            v-model="selectedCategory"
            placeholder="Category"
            :items="categories"
            clearable
            clear-icon="tabler-x"
            />
            </VCol>
          -->

          <!-- 👉 Select Stock Status -->
          <!--
            <VCol
            cols="12"
            sm="4"
            >
            <AppSelect
            v-model="selectedStock"
            placeholder="Stock"
            :items="stockStatus"
            clearable
            clear-icon="tabler-x"
            />
            </VCol>
          -->
        </VRow>
      </VCardText>

      <VDivider />

      <div class="d-flex flex-wrap gap-4 ma-6">
        <div class="d-flex align-center">
          <!-- 👉 Search  -->
          <AppTextField
            v-model="searchQuery"
            placeholder="Search Careers"
            style="inline-size: 200px;"
            class="me-3"
          />
        </div>

        <VSpacer />
        <div class="d-flex gap-4 flex-wrap align-center">
          <AppSelect
            v-model="itemsPerPage"
            :items="[5, 10, 20, 25, 50]"
          />
          <!-- 👉 Export button -->
          <!--
            <VBtn
            variant="tonal"
            color="secondary"
            prepend-icon="tabler-upload"
            >
            Export
            </VBtn>

            <VBtn
            color="primary"
            prepend-icon="tabler-plus"
            @click="$router.push('/apps/ecommerce/product/add')"
            >
            Add Product
            </VBtn>
          -->
        </div>
      </div>

      <VDivider class="mt-4" />

      <!-- 👉 Datatable  -->
      <VDataTableServer
        :headers="headers"
        show-select
        :items="careers"
        :items-length="totalCareers"
      >
        <!-- product  -->
        <!--
          <template #item.product="{ item }">
          <div class="d-flex align-center gap-x-4">
          <VAvatar
          v-if="item.image"
          size="38"
          variant="tonal"
          rounded
          :image="item.image"
          />
          <div class="d-flex flex-column">
          <span class="text-body-1 font-weight-medium text-high-emphasis">{{ item.productName }}</span>
          <span class="text-body-2">{{ item.productBrand }}</span>
          </div>
          </div>
          </template>
        -->

        <!-- category -->
        <!--
          <template #item.category="{ item }">
          <VAvatar
          size="30"
          variant="tonal"
          :color="resolveCategory(item.category)?.color"
          class="me-4"
          >
          <VIcon
          :icon="resolveCategory(item.category)?.icon"
          size="18"
          />
          </VAvatar>
          <span class="text-body-1 text-high-emphasis">{{ item.category }}</span>
          </template>
        -->

        <!-- stock -->
        <!--
          <template #item.stock="{ item }">
          <VSwitch
          :id="useId()"
          :model-value="item.stock"
          />
          </template>
        -->

        <!-- status -->
        <!--
          <template #item.status="{ item }">
          <VChip
          v-bind="resolveStatus(item.status)"
          density="default"
          label
          size="small"
          />
          </template>
        -->

        <!-- Actions -->
        <template #item.actions>
          <IconBtn>
            <VIcon icon="tabler-edit" />
          </IconBtn>

          <IconBtn>
            <VIcon icon="tabler-dots-vertical" />
            <VMenu activator="parent">
              <VList>
                <VListItem
                  value="download"
                  prepend-icon="tabler-download"
                >
                  Download
                </VListItem>

                <VListItem
                  value="duplicate"
                  prepend-icon="tabler-copy"
                >
                  Duplicate
                </VListItem>
              </VList>
            </VMenu>
          </IconBtn>
        </template>

        <!-- pagination -->
        <template #bottom>
          <TablePagination
            v-model:page="page"
            :items-per-page="itemsPerPage"
            :total-items="totalCareers"
          />
        </template>
      </VDataTableServer>
    </VCard>
  </div>
</template>
