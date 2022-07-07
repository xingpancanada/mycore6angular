import { IProduct } from './product';
import {v4 as uuidv4} from 'uuid';

export interface IBasket {
    id: string;
    items: IBasketItem[];
    //id: number;
    clientSecret?: string;
    paymentIntentId?: string;
    deliveryMethodId?: number;
    shippingPrice?: number;
}

export interface IBasketItem {
    id?: number;
    product: IProduct;
    productId: number;
    // productName: string;
    // price: number;
    quantity: number;
    // pictureUrl: string;
    // brand: any;
    // type: any;
}

// export class Basket implements IBasket {
//     uid = uuidv4();
//     products: IProduct[] = [];  //if not init [] -> undefined
// }

export interface IBasketTotals {
    shipping: number;
    subtotal: number;
    total: number;
}
