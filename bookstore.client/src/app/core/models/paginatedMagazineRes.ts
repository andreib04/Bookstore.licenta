import {Magazine} from './magazine';

export interface PaginatedMagazineRes{
  items: Magazine[];
  totalCount: number;
}
