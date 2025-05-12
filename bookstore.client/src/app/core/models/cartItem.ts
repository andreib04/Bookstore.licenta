export interface CartItem {
  productId: number;
  productType: 'Book' | 'Magazine';
  quantity: number;

  title: string;
  description: string;
  price: number;
  image: string;
  subtotal: number;
}
