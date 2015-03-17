'use strict';
var userApp = angular.module('userApp', [
    'ngCookies',
    'ngRoute',
    'ngResource',
    'ngAnimate', 'ngSanitize', 'ui.select', 'ngTagsInput'
]).value('$path', {
    //rest: "/userappREST",
    template: "../../Scripts/common/templates/"
});

userApp.config(function ($httpProvider) {

    /* http interceptors */
    $httpProvider.defaults.timeout = 1000;
    $httpProvider.interceptors.push(function ($q, $rootScope) {
        return {
            requestError: function () {
                console.log("@request: error", arguments);
            },
            responseError: function (response) {

                var notification = {
                    type: 'danger',
                    title: response.status + ' - ' + response.statusText,
                    message: 'Unable to get response from: ' + response.config.url
                };

                if ('data' in response) {
                    notification.details = [];
                    if (typeof response.data != 'object') {
                        response.data = [response.data];
                    }
                    angular.forEach(angular.copy(response.data), function (error) {
                        if (error.message != null) {
                            notification.details.push(error.message);
                        }
                    });
                }
                if (response.message != null) {
                    message += " <b>" + response.message + "</b>"
                }
                console.log(response);
                $rootScope.$notifications.push(notification);
                return $q.reject(response);
            }
        }
    });

});
userApp.factory('$response', function () {
    return {
        to_resource: function (json, resource) {
            try {
                var collection = angular.fromJson(json);
                angular.forEach(collection.items, function (item, id) {
                    collection.items[id] = new resource(item);
                });
                return collection;
            } catch (e) {

            }
        }
    }
});
userApp.run(function ($rootScope,
                $routeParams,
                $location
    //$user
                ) {

    /* Grid objects */
    $rootScope.grids = {};
    /* User data */
    $rootScope.user = {};

    $rootScope.$notifications = [];
    $rootScope.$notifications.$remove = function (index) {
        $rootScope.$notifications.splice(index, 1);
    }

    /* Redirect to search controller */
    $rootScope.search = function (query) {
        $location.path("/search/" + query);
    };

    /* Logout action */
    $rootScope.logout = function () {
        $user.logout(function (response) {
            $location.path("/");
        });
        window.location = '/sunloans/html/';
    };

    $rootScope.$on('$locationChangeSuccess', function () {
        $("html, body").animate({ scrollTop: 0 });
    });

    $rootScope.alert = function (message, title) {
        var dialog = angular.element('#modal-global-info');
        $rootScope.alertDialog = { message: message, title: title || false };
        dialog.modal('show');
    }
    /* Get user data */
    //$user.get(function (response) {

    //    $rootScope.user = response;
    //    console.log("@user:", response);

    //});

});



//function processErrors(scopeErrors, status, responseErrors, customTexts) {
//    if (responseErrors.ErrorMessage && responseErrors.ErrorMessage.length > 0) {
//        scopeErrors.push(
//        {
//            ErrorId: "",
//            Message: responseErrors.ErrorMessage
//        });
//    }

//    if (status == "400" && responseErrors.Errors) {
//        var fields = responseErrors.Errors;
//        for (var field in fields) {
//            if (fields.hasOwnProperty(field)) {
//                var fieldErrors = fields[field];
//                for (var e in fieldErrors) {
//                    if (fieldErrors.hasOwnProperty(e)) {
//                        scopeErrors.push(
//                        {
//                            ErrorId: fieldErrors[e].ErrorId,
//                            Message: formatError(customTexts, field, fieldErrors[e].ErrorMessage)
//                        });
//                    }
//                }
//            }
//        }
//    }

//    if (scopeErrors.length == 0) {
//        alert("Status Code:", status);
//        alert(responseErrors);
//    }
//}

//function formatError(texts, fieldName, errorString) {
//    for (var text in texts) {
//        if (text == fieldName.toLowerCase()) {
//            return errorString.replace("{0}", texts[text]);
//        }
//    }
//    return errorString.replace("{0}", fieldName);
//}