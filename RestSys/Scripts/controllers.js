var ModelId;
function ProductsController($scope, $http) {
    $http.get("/Products/GetStocks/" + ModelId).success(function (data) {
        $scope.stocks = data;
    });

    $scope.add = function () {
        var id = $(".selAddStock").val();
        var title = $(".selAddStock").text();

        $scope.stocks.push({ 'id': id, 'title': title });
        $scope.$apply();

        $.post("/Products/AddStock/" + ModelId, { "stockId": id }).fail(function () {
            //TODO Error handling
        });
    };

    $scope.remove = function (stock) {
        if ($scope.stocks.indexOf(stock) > -1) {
            $scope.stocks.splice($scope.stocks.indexOf(stock), 1);
            $scope.$apply();
        }
        $.post("/Products/RemoveStock/" + ModelId, { "stockId": stock.id }).fail(function () {
            //TODO Error handling
        });
    };
}
;
//# sourceMappingURL=controllers.js.map
