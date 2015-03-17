
userApp.controller('dashboard', function ($scope, $storage, $rootScope, $routeParams, $location, $timeout, $apigeonames,$salt_client, $window) {

    $scope.today = new Date();
    $scope.employee_create_page = "/employees/create/";

    $scope.availableColorsList = ['Red', 'Green', 'Blue', 'Yellow', 'Magenta', 'Maroon', 'Umbra', 'Turquoise'];
    $scope.macaddress = ['ec:1a:59:61:07:b2', '90:59:af:3d:6d:bc', '3c:97:0e:48:22:12', '00:18:31:87:8f:b0', '00:15:17:5f:d2:80', '52:54:00:70:04:02'];

    $scope.application = {};
    $scope.application.state = "Green";//['Blue','Red','Green'];

    $scope.tags = [
    { text: 'Tag1' },
    { text: 'Tag2' },
    { text: 'Tag3' }
    ];

    //autocomplete:
    $scope.states = ['Alabama', 'Alaska', 'Arizona', 'Arkansas', 'California', 'Colorado', 'Connecticut', 'Delaware', 'Florida', 'Georgia', 'Hawaii', 'Idaho', 'Illinois', 'Indiana', 'Iowa', 'Kansas', 'Kentucky', 'Louisiana', 'Maine', 'Maryland', 'Massachusetts', 'Michigan', 'Minnesota', 'Mississippi', 'Missouri', 'Montana', 'Nebraska', 'Nevada', 'New Hampshire', 'New Jersey', 'New Mexico', 'New York', 'North Dakota', 'North Carolina', 'Ohio', 'Oklahoma', 'Oregon', 'Pennsylvania', 'Rhode Island', 'South Carolina', 'South Dakota', 'Tennessee', 'Texas', 'Utah', 'Vermont', 'Virginia', 'Washington', 'West Virginia', 'Wisconsin', 'Wyoming'];
    // Any function returning a promise object can be used to load values asynchronously

    $scope.names = ["john", "bill", "charlie", "robert", "alban", "oscar", "marie", "celine", "brad", "drew", "rebecca", "michel", "francis", "jean", "paul", "pierre", "nicolas", "alfred", "gerard", "louis", "albert", "edouard", "benoit", "guillaume", "nicolas", "joseph"];

    $scope.data = $storage.get('clientsystems');

    $scope.save = function (data) {

        $scope.client_system = angular.copy(data);
        $storage.put('clientsystems', $scope.client_system);
        debugger;

    }

    $scope.searchQuery = '';
    $scope.currentQuery = "";

    $scope.getData = function () {
        debugger
        $scope.currentPage = 0;
        $scope.maxdata = false;
        //var _url = $scope.getListUrl() + '?sort=' + $scope.sortField + '&query=' + $scope.searchQuery + '&pageSize=' + $scope.pageSize + '&pageIndex=' + $scope.currentPage;
        //$http.get(_url).success(function (data) {
        //    if (data.length == 0) { $scope.maxdata = true; }
        //    $scope.billTos = data.BillToList;
        //    $scope.totalRecords = data.TotalRecords;
        //});
        $apigeonames.query({ countryName: $scope.currentQuery }, function (phones) {
            debugger
            //$scope.grids.collaterals.$data(phones);
        });
    }

    $scope.changeQuery = function () {
       
        if ($scope.currentQuery.length < 4) {
            $scope.searchQuery = '';
        } else {
            $scope.searchQuery = $scope.currentQuery;
        }
        debugger
        //reset page:
        $scope.currentPage = 0;
        if ($scope.lastSearchQuery == $scope.searchQuery) { return; }
        $scope.lastSearchQuery = $scope.searchQuery;
        $scope.getData();
        
    }

    //$apigeonames.query({ countryName: $scope.querySearch }, function (phones) {
    //    debugger
    //    //$scope.grids.collaterals.$data(phones);
    //});

    
    ////$salt_client.GetClientsList(function (response) {
        
    ////    //console.log(response.BillTos.$values);
    ////    //$scope.applications = response.$apigeonames.$values;
    ////    //var data = JSON.stringify(response.BillTos.$values);
    ////    //$scope.grids.billtos.$add(data);
    ////    debugger
    ////    $scope.grids.saltclient.$data(response);

    ////});

    $apigeonames.get({ }, function (response) {
        
        //console.log(response.BillTos.$values);
        //$scope.applications = response.$apigeonames.$values;
        //var data = JSON.stringify(response.BillTos.$values);
        //$scope.grids.billtos.$add(data);

//$scope.grids.api_geo_names.$data(response.geonames);

    });

    $rootScope.$on('$viewContentLoaded', function () {
        debugger
        $templateCache.removeAll();


    });

    $scope.metrics = [{
        "delinquencyLatencyTypeEnum": "slow",
        "balance": 310897.40,
        "count": 413,
        "id": 2
    }, {
        "delinquencyLatencyTypeEnum": "30s",
        "balance": 59154.66,
        "count": 98,
        "id": 3
    }, {
        "delinquencyLatencyTypeEnum": "60s",
        "balance": 33498.26,
        "count": 61,
        "id": 4
    }, {
        "delinquencyLatencyTypeEnum": "90s",
        "balance": 25787.86,
        "count": 45,
        "id": 5
    }, {
        "delinquencyLatencyTypeEnum": "91+",
        "balance": 183656.01,
        "count": 324,
        "id": 6
    }];

    /* set template subviews */
    //$scope.templates = {
    //    stream: "App_Scripts/common/templates/stream.html",
    //    modal: "../App_Scripts/common/templates/drawer.html",
    //    loan: "../App_Scripts/common/templates/make_loan_confirm.html"
    //};


    $scope.SubmitEmployees = function (data) {
        /* show sucess message */
        $rootScope.$notifications.push({
            type: 'success',
            title: 'Borrower Payment collection.',
            message: 'The borrower payment collection data was posted succesfully.'
        });
    }

    
    $scope.remove = function(model, e)
    {
        debugger


    }
    $scope.open = function (model, e) {

        //  $scope['billtos'].$selected = $scope.grids['billtos'].$open(e);

        var data = $scope.grids.not_completed.$get(e);
        console.log("@billtos", data);
        //$location.path('/employees/create/' + data.BillToId);
        $window.location.href = $scope.employee_create_page + data.BillToId;


        //// $scope.billtos.$selected = $scope.grids.billtos.$open(e);
        //var data = $scope.grids.not_completed.$get(e);
        //$storage.put('billtos', data);
        //console.log("@billtos", data);
        //$location.path('../billtos/edit');

    };


    //$scope.$on('$viewContentLoaded', function () {
        
        console.log("@init: dashboard"); 

        $scope.applications = {
            not_completed: []
        };


    

        /* get applications in review and fill the datatable*/
        //$billtos.get({ method: "billtos" }, function (response) {
             
        //    //console.log(response.BillTos.$values);
        //    $scope.applications = response.BillTos.$values;
        //    var data = JSON.stringify(response.BillTos.$values);
        //    //$scope.grids.billtos.$add(data);
             
        //    $scope.grids.not_completed.$data(response.BillTos.$values);

        //});

   

});