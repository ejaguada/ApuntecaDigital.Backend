export interface ECommerceProduct {
  id: number
  productName: string
  category: string
  stock: boolean
  sku: number
  price: string
  qty: number
  status: string
  image: string
  productBrand: string
}

export interface ECareerData {
  id: number
  name: string
  classes:
  [
    id: number,
    name: string,
    year: number,
    career:
    {
      id: number
      name: string
    },
  ]
}
