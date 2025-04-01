import {Category} from './category';

export interface Item{
  id: number;
  title: string;
  description: string;
  price: number;
  stock: number;
  itemType: string;
  image: string;
  category: Category;
}
