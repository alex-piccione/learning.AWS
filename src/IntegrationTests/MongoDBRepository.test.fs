namespace IntegrationTests

open System
open NUnit.Framework
open FsUnit
open Learning.Portfolio


[<TestFixture; Category("Database")>]
type MongoDBFundRepositoryTest() =

    let repository = new MongoDBFundRepository(secrets.connectionString) //:> IFundRepository
    let TEST_ID = "test"

    let delete id = 
        try 
            repository.Delete(id)
        with exc -> raise(Exception($"Failed to delete record with id={id}.", exc))

    let getSingle id = 
        try 
            repository.Get(id)
        with exc -> raise(Exception($"Failed to read record with id={id}.", exc))


    [<SetUp>]
    member this.Setup () =
        ()

    [<TearDown>]
    member this.TearDown () =
        delete TEST_ID



    [<Test>]
    member this.``Create`` () =

        let fund = new Fund(TEST_ID, "test name", "TEST")
        // execute
        repository.Create(fund)

        let createdFund = getSingle fund.Id

        createdFund |> should not' (be Null)
        createdFund.Id |> should equal fund.Id
        createdFund.Name |> should equal fund.Name
        createdFund.Code |> should equal fund.Code

    [<Test>]
    member this.Get () =

        let fund = new Fund(TEST_ID, "test name", "TEST")
        repository.Create(fund)

        // execute
        let createdFund = repository.Get(fund.Id)

        createdFund |> should not' (be Null)
        createdFund.Id |> should equal fund.Id
        createdFund.Name |> should equal fund.Name
        createdFund.Code |> should equal fund.Code
