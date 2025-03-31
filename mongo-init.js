db = db.getSiblingDB('OrderList');

print("Creating collection 'Orders'...");
db.createCollection('Orders');
print("Collection 'Orders' created.");

const firstNames = ['Adam Dobson', 'John Smith', 'Emma Brown', 'Olivia Johnson', 'Liam Williams'];
const addresses = ['Paris', 'Moscow', 'New York', 'London', 'Berlin'];
const products = ['Samsung Galaxy S11', 'iPhone 16', 'Apple Watch'];
const deliveryMethods = ['To the door', 'Pick up from point'];

const documents = [];

print("Generating test data...");
for (let i = 0; i < 25; i++) {
    const document = {
        firstName: firstNames[Math.floor(Math.random() * firstNames.length)],
        address: addresses[Math.floor(Math.random() * addresses.length)],
        product: products[Math.floor(Math.random() * products.length)],
        deliveryDate: '2025-03-28',
        deliveryTime: '17:52',
        deliveryMethod: deliveryMethods[Math.floor(Math.random() * deliveryMethods.length)],
        sendNotification: false,
        dateCreated: new Date()
    };
    documents.push(document);
}
print("Test data generated. Inserting into 'Orders'...");

db.Orders.insertMany(documents);

print("25 test documents inserted successfully.");
