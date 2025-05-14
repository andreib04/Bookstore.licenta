import {OrderItem} from './OrderItem';

export interface Order{
  id: number;
  userId: number;
  items: OrderItem[];
  total: number;
  status: string;
  createdAt: Date;
  address: string;
  city: string;
  postalCode: string;
  country: string;
}
