## Swagger Tests Cases:

Order request provided to test scenarios (feel free to create your own)

- **Customer:** Alice Johnson  
- **Order Items:**  
  - 1x Margherita Pizza  
  - 2x Coca-Cola 350ml  
### First Scenario: Valid order with 2 items

```
{
  "customerId": "7f7de9a5-d122-4fe6-9044-054c61d84c2c",
  "items": [
    {
      "productId": "892d357b-7b2f-40c5-bd25-85abf0f7c845",
      "quantity": 1
    },
    {
      "productId": "992b48c4-28c9-4263-89b1-45b72ab59a05",
      "quantity": 2
    }
  ]
}
```

**HTTP Response:** 201 <br>
**Message:** /api/order/`orderId`

### Second Scenario: Invalid order item quantity

```
{
  "customerId": "7f7de9a5-d122-4fe6-9044-054c61d84c2c",
  "items": [
    {
      "productId": "892d357b-7b2f-40c5-bd25-85abf0f7c845",
      "quantity": 0
    }
  ]
}
```

```
{
  "customerId": "7f7de9a5-d122-4fe6-9044-054c61d84c2c",
  "items": [
    {
      "productId": "892d357b-7b2f-40c5-bd25-85abf0f7c845",
      "quantity": -2
    }
  ]
}
```
**HTTP Response:** 400 <br>
**Message:** The item quantity must be greater than zero

### Third  Scenario: Invalid product
```
{
  "customerId": "7f7de9a5-d122-4fe6-9044-054c61d84c2c",
  "items": [
    {
      "productId": "892d357b-7b2f-40c5-bd25-85abf0f7c845",
      "quantity": 1
    },
    {
      "productId": "e43408ea-7293-4cf6-9ee2-33d03eb487a0",
      "quantity": 2
    }
  ]
}
```
**HTTP Response:** 400 <br>
**Message:** Item with ID `e43408ea-7293-4cf6-9ee2-33d03eb487a0` does not exist on database

### Fourth  Scenario: Invalid customer
```
{
  "customerId": "e43408ea-7293-4cf6-9ee2-33d03eb487a0",
  "items": [
    {
      "productId": "892d357b-7b2f-40c5-bd25-85abf0f7c845",
      "quantity": 1
    },
    {
      "productId": "992b48c4-28c9-4263-89b1-45b72ab59a05",
      "quantity": 2
    }
  ]
}
```

**HTTP Response:** 400 <br>
**Message:** Customer not registered in the system