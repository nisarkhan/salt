
userApp.controller('BillingInformationCtrl', function ($scope, $location, $rootScope, $routeParams, $billinginformation) {


    $billinginformation.GetBillingInformationList(function (response) {

         
        $scope.grids.billinginfo.$data(response);

    });


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