IF NOT EXISTS (SELECT 1 FROM Product)
    BEGIN
        INSERT INTO Product (Id, Name, Description, Price, Category)
        VALUES
            (NEWID(), 'Margherita Pizza', 'Classic pizza with tomatoes, mozzarella, and basil', 29.90, 0), -- PIZZA
            (NEWID(), 'Pepperoni Pizza', 'Pizza topped with pepperoni and mozzarella', 34.90, 0),
            (NEWID(), 'Four Cheese Pizza', 'Blend of mozzarella, parmesan, gorgonzola, and provolone', 39.90, 0),
            (NEWID(), 'Veggie Pizza', 'Pizza loaded with vegetables', 32.90, 0),
        
            (NEWID(), 'Coca-Cola 350ml', 'Refreshing soda can', 5.90, 1), -- DRINK
            (NEWID(), 'Orange Juice 500ml', 'Freshly squeezed orange juice', 9.90, 1),
            (NEWID(), 'Sparkling Water 500ml', 'Carbonated mineral water', 6.90, 1),
            (NEWID(), 'Red Wine Glass', 'House red wine (glass)', 19.90, 1),
        
            (NEWID(), 'Chocolate Cake', 'Rich and moist chocolate cake slice', 14.90, 2), -- DESSERT
            (NEWID(), 'Tiramisu', 'Classic Italian dessert with mascarpone and coffee', 17.90, 2),
            (NEWID(), 'Ice Cream (2 scoops)', 'Choice of chocolate, vanilla, or strawberry', 12.90, 2),
            (NEWID(), 'Cheesecake', 'Creamy New York-style cheesecake slice', 15.90, 2);
    END

IF NOT EXISTS (SELECT 1 FROM dbo.Customer)
    BEGIN
        INSERT INTO dbo.Customer (Id, Name, Email, PhoneNumber)
        VALUES 
            (NEWID(), 'Alice Johnson', 'alice.johnson@example.com', '99999-0001'),
            (NEWID(), 'Bruno Souza', 'bruno.souza@example.com', '99999-0002'),
            (NEWID(), 'Carla Mendes', 'carla.mendes@example.com', '99999-0003');
    END