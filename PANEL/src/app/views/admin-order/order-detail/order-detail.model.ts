export interface OrderDetail {
    id: number;
    orderNumber: string | null;
    confirmation: boolean | null;
    totalAmount: number;
    openAccountOrder: boolean;
    paymentStatus: boolean;
    dealerId: number;
    billId: number | null;
    paymentId: number | null;
    orderItems: OrderItem[];
  }
  
  export interface OrderItem {
    quantity: number;
    unitPrice: number;
    totalPrice: number;
    orderId: number;
    productId: number;
    product: any | null; // You can define a Product interface if needed
    id: number;
    createdById: number;
    insertDate: string;
    updatedById: number;
    updateDate: string | null;
    isActive: boolean;
  }
  