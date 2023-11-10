import { INavData } from '@coreui/angular';


function generateNavItems(Role: string): INavData[] {
  const navItems: INavData[] = [];

  if(Role === "dealer"){
    navItems.push(
      {
        title: true,
        name: 'Dealer Informations'
      },
      {
        name: 'Cards',
        url: '/card',
        iconComponent: { name: 'cil-credit-card' },
        children: [
          {
            name: 'Add',
            url: '/card/add'
          },
          {
            name: 'List',
            url: '/card/list'
          },
        ]
      },
      {
        name: 'Address',
        url: '/address',
        iconComponent: { name: 'cil-home' },
        children: [
          {
            name: 'Add',
            url: '/address/add'
          },
          {
            name: 'List',
            url: '/address/list'
          },
        ]
      },
      {
        title: true,
        name: 'Order Operations'
      },
      {
        name: 'Order',
        url: '/order',
        iconComponent: { name: 'cil-puzzle' },
        children: [
          {
            name: 'Create Order',
            url: '/order/createorder'
          },
          {
            name: 'List Orders',
            url: '/order/list'
          },
          {
            name: 'List Payments',
            url: '/order/payment-list'
          },
        ]
      },
    )
  }

  if(Role === "admin"){
    navItems.push(
      {
        title: true,
        name: 'Dealer Operations'
      },
      {
        name: 'Dealers',
        url: '/dealers',
        iconComponent: { name: 'cil-user' },
        children: [
          {
            name: 'List',
            url: '/dealers/list'
          },
        ]
      },
      {
        title: true,
        name: 'Inventory Management'
      },
      {
        name: 'Categories',
        url: '/category',
        iconComponent: { name: 'cil-puzzle' },
        children: [
          {
            name: 'Add',
            url: '/category/add'
          },
          {
            name: 'List',
            url: '/category/list'
          },
        ]
      },
      {
        name: 'Products',
        url: '/product',
        iconComponent: { name: 'cil-puzzle' },
        children: [
          {
            name: 'Add',
            url: '/product/add'
          },
          {
            name: 'List',
            url: '/product/list'
          },
        ]
      },
      {
        title: true,
        name: 'Orders'
      },
      {
        name: 'Orders',
        url: '/admin-order',
        iconComponent: { name: 'cil-puzzle' },
        children: [
          {
            name: 'Order List',
            url: '/admin-order/list'
          },
        ]
      },
    )
  }

  return navItems;
}

export { generateNavItems };