
userApp.controller('VendorCtrl', function ($scope, $location, $rootScope, $routeParams, $vendor) {


    $vendor.GetVendorList(function (response) {

        //console.log(response.BillTos.$values);
        //$scope.applications = response.$apigeonames.$values;
        //var data = JSON.stringify(response.BillTos.$values);
        //$scope.grids.billtos.$add(data);
         
        var data = JSON.stringify(response);
        $scope.grids.gvvendor.$data(response);

    });


    /* review application */
    $scope.open = function (e) {
         var data = $scope.grids.gvvendor.$get(e);
        //$location.path('Clients/Edit/' + data.UserId);// + (section ? '/' + section : ''));
        window.location.href = 'Vendor/Edit/' + data.VendorPKID;
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