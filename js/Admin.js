var QsContent = QsContent || {};

QsContent.module = angular.module('QsContent', []);


//Angular Filter for Truncation
QsContent.module.filter('truncate', function () {
    return function (text, length, end) {
        if (isNaN(length))
            length = 10;

        if (end === undefined)
            end = "...";

        if (text.length <= length || text.length - end.length <= length) {
            return text;
        }
        else {
            return String(text).substring(0, length - end.length) + end;
        }

    };
});

//Angular Service - WebService 
QsContent.module.service('WebService', function ($http, $q) {
 return {
        GetAdmin: function() {
            var defer = $q.defer();

            $http.post(QsContent.options.ServicePath + "/GetAdmin",
                    { tabId: QsContent.options.TabId, moduleId: QsContent.options.ModuleId },
                    { headers: { 'Content-Type': 'application/json' } })
                .success(function(data) {
                    defer.resolve(data);
                }).error(function() {
                    alert("Error!");
                });

            return defer.promise;
        },
        SaveCheck: function(checkInfo) {
            var defer = $q.defer();
            delete checkInfo.CreatedOnDate;
            delete checkInfo.__type;
            $http.post(QsContent.options.ServicePath + "/SaveCheck",
                    { tabId: QsContent.options.TabId, moduleId: QsContent.options.ModuleId, checkInfo: checkInfo },
                    { headers: { 'Content-Type': 'application/json' } })
                .success(function(data) {
                    defer.resolve(data);
                }).error(function() {
                    alert("Error!");
                });

            return defer.promise;
        },
        DeleteCheck: function(checkId) {
            var defer = $q.defer();
            $http.post(QsContent.options.ServicePath + "/DeleteCheck",
                    { tabId: QsContent.options.TabId, moduleId: QsContent.options.ModuleId, checkId:  checkId},
                    { headers: { 'Content-Type': 'application/json' } })
                .success(function (data) {
                    defer.resolve(data);
                }).error(function () {
                    alert("Error!");
                });

            return defer.promise;

        }
    };
});

//Angular Controller Function
function QsAdminCtl($scope,WebService) {
    var defaults = {
        Querystring: '',
        HideMatch: false,
        ServerSide: false,
    };
    $scope.qs = defaults;
    $scope.ButtonText = "Add";
    WebService.GetAdmin().then(function (data) {
        $scope.modules = data.d.Modules;
        $scope.checkValues = data.d.CheckValues;
    });

    $scope.SaveCheck = function (evt) {
        evt.preventDefault();
        WebService.SaveCheck($scope.qs).then(function (data) {
            $scope.checkValues = data.d.CheckValues;
        });
        $scope.qs = {};
        $scope.qs.Querystring = "";
        $scope.qs.HideMatch = false;
        $scope.qs.ServerSide = false;
        $scope.qs.CheckModuleID = 0;
    };

    $scope.DeleteCheck = function(evt) {
        evt.preventDefault();
        var index = this.$index;
        if (confirm("Are you sure you want to delete?")) {
            WebService.DeleteCheck(this.check.ID).then(function(data) {
                if (data.d) {
                    $scope.checkValues.splice(index, 1);
                }
            });
        }
    };

    $scope.CancelSelection = function(evt) {
        evt.preventDefault();
        $scope.qs = {};
        $scope.ButtonText = "Add";
    };

    $scope.SelectCheck = function(evt) {
        evt.preventDefault();
        $scope.qs = this.check;
        $scope.ButtonText = "Update";
    };

    $scope.getModuleTitle = function (moduleId) {
        var name = "";
        $scope.modules.forEach(function (o) {
            if (o.ModuleId == moduleId) {
                name = o.Name;
            }
        });
        return name;
    };
}
