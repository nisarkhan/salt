
userApp.controller('ClientCtrl', function ($scope, $location, $rootScope, $routeParams, $client) {


    $client.GetVendorList(function (response) {

        //console.log(response.BillTos.$values);
        //$scope.applications = response.$apigeonames.$values;
        //var data = JSON.stringify(response.BillTos.$values);
        //$scope.grids.billtos.$add(data);
 
        $("#client_processing").css("visibility", "visible");
        $scope.grids.client.$data(response);

    });

    $scope.save = function (data)
    {
        $rootScope.$notifications.push({
            type: 'success',
            title: 'Borrower Demographic.',
            message: 'The borrower demographic data was updated succesfully.'
        });

    }

    /* review application */
    $scope.open = function (e) {
     debugger
        var data = $scope.grids.client.$get(e);
        //$location.path('Clients/Edit/' + data.UserId);// + (section ? '/' + section : ''));
        window.location.href = 'Client/Edit/' + data.CustomerID;
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