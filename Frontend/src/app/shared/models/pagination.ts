/////85. Creating a products interface

import {IProduct} from './product';

export interface IPagination{
    brandId: number;
    typeId: number;
    page: number;
    pageSize: number;
    count: number;
    products: IProduct[];
}

export class Pagination implements IPagination {
    brandId: number = 0;
    typeId: number = 0;
    page: number = 1;
    pageSize: number = 100;
    count: number = 0;
    products: IProduct[] = [];
}
