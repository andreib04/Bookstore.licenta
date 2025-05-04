import {Magazine} from './magazine';

export interface PaginatedMagazineRes{
  magazines: Magazine[];
  totalCount: number;
}
