namespace IntegrationTests

open System
open System.Linq
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

        let fund = Fund(TEST_ID, "test name", "TEST")
        // execute
        repository.Create(fund)

        let createdFund = getSingle fund.Id

        createdFund |> should not' (be Null)
        createdFund.Id |> should equal fund.Id
        createdFund.Name |> should equal fund.Name
        createdFund.Code |> should equal fund.Code

    [<Test>]
    member this.Get () =

        let fund = Fund(TEST_ID, "test name", "TEST")
        repository.Create(fund)

        // execute
        let createdFund = repository.Get(fund.Id)

        createdFund |> should not' (be Null)
        createdFund.Id |> should equal fund.Id
        createdFund.Name |> should equal fund.Name
        createdFund.Code |> should equal fund.Code

    [<Test>]
    member this.List () =

        let fund1 = Fund(TEST_ID+"1", "test name", "TEST 1")
        let fund2 = Fund(TEST_ID+"2", "test name", "TEST 2")
        delete fund1.Id
        delete fund2.Id
        repository.Create(fund1)
        repository.Create(fund2)

        try 
            // execute
            let funds = repository.List()
            //funds |> should contain fund1
            //funds |> should contain fund2
            Assert.IsTrue( funds.SingleOrDefault(fun f -> f.Id = fund1.Id) <> null)
            Assert.IsTrue( funds.SingleOrDefault(fun f -> f.Id = fund2.Id) <> null)
        finally
            delete fund1.Id
            delete fund2.Id


    [<Test>]
    member this.Delete () =

        let fund = Fund(TEST_ID, "test name", "TEST")
        repository.Create(fund)

        // execute
        repository.Delete(fund.Id)

        let deletedFund = repository.Get(fund.Id)
        deletedFund |> should be null
