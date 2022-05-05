function spCreateOrderWithAmount(orderToCreate) {
    orderToCreate.Customer.Address.isUKAddress =
        orderToCreate.Customer.Address.PostCode == "1000";

    var context = getContext();
    var collection = context.getCollection();
    var collectionLink = collection.getSelfLink();
    tryCreate(orderToCreate, callback);

    function tryCreate(item, callback) {
        var options = { disableAutomaticIdGeneration: false };
        var isAccepted = collection.createDocument(collectionLink, item, options, callback);
        if (!isAccepted) getContext().getResponse().setBody(item);
    }

    function callback(err, item, options) {
        if (err) throw err;
        getContext().getResponse().setBody(item);
    }
}