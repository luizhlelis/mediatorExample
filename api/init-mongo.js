db.CreateUser(
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