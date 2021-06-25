namespace Learning.Portfolio {
    // interface is stored within the concrete class because it only exists to make possible injection for test purpose
    interface IFundRepository {
        Fund Create(Fund fund);
    }

    class FundRepository :IFundRepository {
        public Fund Create(Fund fund){

            // TODO: store
            return fund;
        }
    }

}