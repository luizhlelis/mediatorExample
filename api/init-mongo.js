db.createUser(
    {
        user: "root",
        pwd: "1Secure*Password1",
        roles: [
            {
                role: "readWrite",
                db: "MediatorExampleNoSql"
            }
        ]
    }
)
db.Customers.insert(
    {
        Id: "dd7b68b4-9bc0-4b9b-81d9-ddede17ce98d",
        Name: "Luiz Lelis",
        Email: "luizhlelis@gmail.com",
        Phone: "(31)99999-9999"
    }
)