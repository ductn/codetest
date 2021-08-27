appShop.controller('TopBanner', function ($scope, $http) {
    var url = "/Home/SliderBanner?IsType=2&Take=2";
    $http.get(url).then(
        function successCallback(response) {
            if (response.data.status) {
                $scope.TopSliders = response.data.data;
            }
        },
        function errorCallback(response) {
            console.log("Unable to perform get request");
        }
    );

});

appShop.controller('BodyBanner', function ($scope, $http) {
    var url = "/Home/SliderBanner?IsType=3&Take=2";
    $http.get(url).then(
        function successCallback(response) {
            if (response.data.status) {
                $scope.BodySliders = response.data.data;
            }
        },
        function errorCallback(response) {
            console.log("Unable to perform get request");
        }
    );

});

//appShop.controller('DangGiamGia', function ($scope, $http) {
//    var url = "/Home/DangGiamGia";
//    $http.get(url).then(
//        function successCallback(response) {
//            if (response.data.status) {
//                $scope.Products = response.data.data;
//            }
//        },
//        function errorCallback(response) {
//            console.log("Unable to perform get request");
//        }
//    );

//});