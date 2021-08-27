appShop.controller('BusinessPartnerController', function ($scope, $http) {
    var url = "/Home/BusinessPartner";
    $http.get(url).then(
        function successCallback(response) {
            if (response.data.status) {
                $scope.BusinessPartners = response.data.data;
            }
        },
        function errorCallback(response) {
            console.log("Unable to perform get request");
        }
    );

});