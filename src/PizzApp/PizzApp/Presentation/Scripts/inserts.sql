IF NOT EXISTS (SELECT 1 FROM Product)
BEGIN
    INSERT INTO Product (Id, Name, Description, Price, Category)
    VALUES
        ('892d357b-7b2f-40c5-bd25-85abf0f7c845', 'Margherita Pizza', 'Classic pizza with tomatoes, mozzarella, and basil', 29.90, 0), -- PIZZA
        ('c1bcfee4-1265-46d0-8fac-dbb8fd903391', 'Pepperoni Pizza', 'Pizza topped with pepperoni and mozzarella', 34.90, 0),
        ('05bcf173-ef4d-44e1-ab91-895a69bda877', 'Four Cheese Pizza', 'Blend of mozzarella, parmesan, gorgonzola, and provolone', 39.90, 0),
        ('55a020fd-db82-4e88-bee5-def9bb467a81', 'Veggie Pizza', 'Pizza loaded with vegetables', 32.90, 0),
        ('992b48c4-28c9-4263-89b1-45b72ab59a05', 'Coca-Cola 350ml', 'Refreshing soda can', 5.90, 1), -- DRINK
        ('b3a45d8e-7026-46df-9292-80f97c3fca26', 'Orange Juice 500ml', 'Freshly squeezed orange juice', 9.90, 1),
        ('066262d3-dff4-43fc-bd65-e84887ec5d2b', 'Sparkling Water 500ml', 'Carbonated mineral water', 6.90, 1),
        ('5dd97543-b1d5-400b-93a4-0c7bdfcce18e', 'Red Wine Glass', 'House red wine (glass)', 19.90, 1),
        ('87c6a48c-e32e-4a5d-a4b7-2a6e8151a05b', 'Chocolate Cake', 'Rich and moist chocolate cake slice', 14.90, 2), -- DESSERT
        ('b79d6251-4ce1-43b3-8bd2-363510d84e12', 'Tiramisu', 'Classic Italian dessert with mascarpone and coffee', 17.90, 2),
        ('b1419698-66ec-4129-a465-a2c85affc626', 'Ice Cream (2 scoops)', 'Choice of chocolate, vanilla, or strawberry', 12.90, 2),
        ('35860bf2-e6a0-4be7-a8e6-f06d20b45295', 'Cheesecake', 'Creamy New York-style cheesecake slice', 15.90, 2);
END

IF NOT EXISTS (SELECT 1 FROM dbo.Customer)
BEGIN
    INSERT INTO dbo.Customer (Id, Name, Email, PhoneNumber)
    VALUES
        ('7f7de9a5-d122-4fe6-9044-054c61d84c2c', 'Alice Johnson', 'alice.johnson@example.com', '99999-0001'),
        ('d74b1c76-83e3-4ee2-a040-4b14cf898860', 'Bruno Souza', 'bruno.souza@example.com', '99999-0002'),
        ('626da320-2252-4cdf-aa94-68fba4b3cf62', 'Carla Mendes', 'carla.mendes@example.com', '99999-0003');
END
