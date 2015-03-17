
userApp.controller('ClientsCtrl', function ($scope, $location, $rootScope, $routeParams, $salt_client) {


    $salt_client.GetClientsList(function (response) {

        //console.log(response.BillTos.$values);
        //$scope.applications = response.$apigeonames.$values;
        //var data = JSON.stringify(response.BillTos.$values);
        //$scope.grids.billtos.$add(data);
         
        $scope.grids.saltclient.$data(response);

    });


    /* review application */
    $scope.open = function (e) {
        debugger
        var data = $scope.grids.saltclient.$get(e);
        //$location.path('Clients/Edit/' + data.UserId);// + (section ? '/' + section : ''));
        window.location.href = 'Clients/Edit/' + data.UserId;
    };

    /* manage call routes:loan */
    $scope.manage = function (e) {
        var data = $scope.grids.routes.$get(e);
        console.log("@loan", data);
        $location.path('/borrower/' + data.account_number).replace();
    };

    
    //////var id = headers('location').split('/').pop();
    
    ////$scope.SubmitEmployees = function (data) {
    ////    /* show sucess message */
    ////    $rootScope.$notifications.push({
    ////        type: 'success',
    ////        title: 'Borrower Payment collection.',
    ////        message: 'The borrower payment collection data was posted succesfully.'
    ////    });
    ////}
  
});