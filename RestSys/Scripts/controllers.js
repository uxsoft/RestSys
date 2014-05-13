var ModelId;

var RestSysAngular = angular.module("RestSysAngular", ["ui.sortable"]);

RestSysAngular.controller("ProductsController", function ($scope, $http) {
    $http.get("/Products/GetStocks/" + ModelId).success(function (data) {
        $scope.stocks = data;
    });

    $scope.add = function () {
        var id = $(".selAddStock").val();
        var title = $(".selAddStock option:selected").text();
        var amount = $("#amount").val();

        $scope.stocks.push({ 'id': id, 'title': title, 'amount': amount });

        $.post("/Products/AddStock/" + ModelId, { "stockId": id, "amount": amount }).fail(function () {
            $http.get("/Products/GetStocks/" + ModelId).success(function (data) {
                $scope.stocks = data;
                $scope.$apply();
            });
        });
    };

    $scope.remove = function (stock) {
        if ($scope.stocks.indexOf(stock) > -1) {
            $scope.stocks.splice($scope.stocks.indexOf(stock), 1);
        }
        $.post("/Products/RemoveStock/" + ModelId, { "stockId": stock.id }).fail(function () {
            $http.get("/Products/GetStocks/" + ModelId).success(function (data) {
                $scope.stocks = data;
                $scope.$apply();
            });
        });
    };
});

RestSysAngular.controller("NavigationController", function ($scope, $http) {
    $http.get("/Navigation/GetChildren/" + ModelId).success(function (data) {
        $scope.children = data;
    });

    $scope.add = function () {
        var id = $(".selAddChild").val();
        var title = $(".selAddChild option:selected").text();

        $scope.children.push({ 'id': id, 'title': title });

        $.post("/Navigation/AddChild/" + ModelId, { "childId": id }).fail(function () {
            $http.get("/Navigation/GetChildren/" + ModelId).success(function (data) {
                $scope.children = data;
                $scope.$apply();
            });
        });
    };

    $scope.remove = function (navigationItem) {
        if ($scope.children.indexOf(navigationItem) > -1) {
            $scope.children.splice($scope.children.indexOf(navigationItem), 1);
        }

        $.post("/Navigation/RemoveChild/" + ModelId, { "childId": navigationItem.id }).fail(function () {
            $http.get("/Navigation/GetChildren/" + ModelId).success(function (data) {
                $scope.children = data;
                $scope.$apply();
            });
        });
    };
});

RestSysAngular.controller("OrdersController", function ($scope, $http) {
    $http.get("/Orders/GetProducts/" + ModelId).success(function (data) {
        $scope.products = data;
    });

    $scope.add = function () {
        var id = $(".selAddProduct").val();
        var title = $(".selAddProduct option:selected").text();

        $scope.products.push({ 'id': id, 'title': title });

        $.post("/Orders/AddProduct/" + ModelId, { "productId": id }).fail(function () {
            $http.get("/Orders/GetProducts/" + ModelId).success(function (data) {
                $scope.products = data;
                $scope.$apply();
            });
        });
    };

    $scope.remove = function (product) {
        if ($scope.products.indexOf(product) > -1) {
            $scope.products.splice($scope.products.indexOf(product), 1);
        }
        $.post("/Orders/RemoveProduct/" + ModelId, { "productId": product.id }).fail(function () {
            $http.get("/Orders/GetProducts/" + ModelId).success(function (data) {
                $scope.products = data;
                $scope.$apply();
            });
        });
    };
});
//# sourceMappingURL=controllers.js.map
