Type.registerNamespace('transchart.net');
transchart.net.idataaccess = function () {
    transchart.net.idataaccess.initializeBase(this);
    this._timeout = 0;
    this._userContext = null;
    this._succeeded = null;
    this._failed = null;
}
transchart.net.idataaccess.prototype = {
    _get_path: function () {
        var p = this.get_path();
        if (p) return p;
        else return transchart.net.idataaccess._staticInstance.get_path();
    },
    GetPeople: function (query, succeededCallback, failedCallback, userContext) {
        return this._invoke(this._get_path(), 'GetPeople', false, { query: query }, succeededCallback, failedCallback, userContext);
    },
    GetPersonByMRN: function (mrn, succeededCallback, failedCallback, userContext) {
        return this._invoke(this._get_path(), 'GetPersonByMRN', false, { mrn: mrn }, succeededCallback, failedCallback, userContext);
    },
    GetPersonBySSN: function (ssn, succeededCallback, failedCallback, userContext) {
        return this._invoke(this._get_path(), 'GetPersonBySSN', false, { ssn: ssn }, succeededCallback, failedCallback, userContext);
    },
    GetPersonByName: function (firstName, lastName, succeededCallback, failedCallback, userContext) {
        return this._invoke(this._get_path(), 'GetPersonByName', false, { firstName: firstName, lastName: lastName }, succeededCallback, failedCallback, userContext);
    }
}
transchart.net.idataaccess.registerClass('transchart.net.idataaccess', Sys.Net.WebServiceProxy);
transchart.net.idataaccess._staticInstance = new transchart.net.idataaccess();
transchart.net.idataaccess.set_path = function (value) { transchart.net.idataaccess._staticInstance.set_path(value); }
transchart.net.idataaccess.get_path = function () { return transchart.net.idataaccess._staticInstance.get_path(); }
transchart.net.idataaccess.set_timeout = function (value) { transchart.net.idataaccess._staticInstance.set_timeout(value); }
transchart.net.idataaccess.get_timeout = function () { return transchart.net.idataaccess._staticInstance.get_timeout(); }
transchart.net.idataaccess.set_defaultUserContext = function (value) { transchart.net.idataaccess._staticInstance.set_defaultUserContext(value); }
transchart.net.idataaccess.get_defaultUserContext = function () { return transchart.net.idataaccess._staticInstance.get_defaultUserContext(); }
transchart.net.idataaccess.set_defaultSucceededCallback = function (value) { transchart.net.idataaccess._staticInstance.set_defaultSucceededCallback(value); }
transchart.net.idataaccess.get_defaultSucceededCallback = function () { return transchart.net.idataaccess._staticInstance.get_defaultSucceededCallback(); }
transchart.net.idataaccess.set_defaultFailedCallback = function (value) { transchart.net.idataaccess._staticInstance.set_defaultFailedCallback(value); }
transchart.net.idataaccess.get_defaultFailedCallback = function () { return transchart.net.idataaccess._staticInstance.get_defaultFailedCallback(); }
transchart.net.idataaccess.set_enableJsonp = function (value) { transchart.net.idataaccess._staticInstance.set_enableJsonp(value); }
transchart.net.idataaccess.get_enableJsonp = function () { return transchart.net.idataaccess._staticInstance.get_enableJsonp(); }
transchart.net.idataaccess.set_jsonpCallbackParameter = function (value) { transchart.net.idataaccess._staticInstance.set_jsonpCallbackParameter(value); }
transchart.net.idataaccess.get_jsonpCallbackParameter = function () { return transchart.net.idataaccess._staticInstance.get_jsonpCallbackParameter(); }
transchart.net.idataaccess.set_path("http://127.0.0.1:3949/TC/dataaccess/rest");
transchart.net.idataaccess.GetPeople = function (query, onSuccess, onFailed, userContext) { transchart.net.idataaccess._staticInstance.GetPeople(query, onSuccess, onFailed, userContext); }
transchart.net.idataaccess.GetPersonByMRN = function (mrn, onSuccess, onFailed, userContext) { transchart.net.idataaccess._staticInstance.GetPersonByMRN(mrn, onSuccess, onFailed, userContext); }
transchart.net.idataaccess.GetPersonBySSN = function (ssn, onSuccess, onFailed, userContext) { transchart.net.idataaccess._staticInstance.GetPersonBySSN(ssn, onSuccess, onFailed, userContext); }
transchart.net.idataaccess.GetPersonByName = function (firstName, lastName, onSuccess, onFailed, userContext) { transchart.net.idataaccess._staticInstance.GetPersonByName(firstName, lastName, onSuccess, onFailed, userContext); }
var gtc = Sys.Net.WebServiceProxy._generateTypedConstructor;
Type.registerNamespace('zen.data.types');
if (typeof (zen.data.types.Query) === 'undefined') {
    zen.data.types.Query = gtc("Query:http://zen.data/types/");
    zen.data.types.Query.registerClass('zen.data.types.Query');
}
Type.registerNamespace('System');
if (typeof (System.Type) === 'undefined') {
    System.Type = gtc("Type:http://schemas.datacontract.org/2004/07/System");
    System.Type.registerClass('System.Type');
}
Type.registerNamespace('System.Reflection');
if (typeof (System.Reflection.MemberInfo) === 'undefined') {
    System.Reflection.MemberInfo = gtc("MemberInfo:http://schemas.datacontract.org/2004/07/System.Reflection");
    System.Reflection.MemberInfo.registerClass('System.Reflection.MemberInfo');
}
Type.registerNamespace('Zen.Data.QueryModel');
if (typeof (Zen.Data.QueryModel.Criterion) === 'undefined') {
    Zen.Data.QueryModel.Criterion = gtc("Criterion:http://schemas.datacontract.org/2004/07/Zen.Data.QueryModel");
    Zen.Data.QueryModel.Criterion.registerClass('Zen.Data.QueryModel.Criterion');
}
if (typeof (Zen.Data.QueryModel.Parameter) === 'undefined') {
    Zen.Data.QueryModel.Parameter = gtc("Parameter:http://schemas.datacontract.org/2004/07/Zen.Data.QueryModel");
    Zen.Data.QueryModel.Parameter.registerClass('Zen.Data.QueryModel.Parameter');
}
if (typeof (Zen.Data.QueryModel.OrderClause) === 'undefined') {
    Zen.Data.QueryModel.OrderClause = gtc("OrderClause:http://schemas.datacontract.org/2004/07/Zen.Data.QueryModel");
    Zen.Data.QueryModel.OrderClause.registerClass('Zen.Data.QueryModel.OrderClause');
}
if (typeof (transchart.net.persondto) === 'undefined') {
    transchart.net.persondto = gtc("PersonDto:http://transchart.net");
    transchart.net.persondto.registerClass('transchart.net.persondto');
}
if (typeof (transchart.net.persondtomin) === 'undefined') {
    transchart.net.persondtomin = gtc("PersonDtoMin:http://transchart.net");
    transchart.net.persondtomin.registerClass('transchart.net.persondtomin');
}
if (typeof (Zen.Data.QueryModel.CriteriaOperators) === 'undefined') {
    Zen.Data.QueryModel.CriteriaOperators = function () { throw Error.invalidOperation(); }
    Zen.Data.QueryModel.CriteriaOperators.prototype = { And: 0, Or: 1, Equal: 2, NotEqual: 3, GreaterThan: 4, LesserThan: 5, GreaterThanOrEqual: 6, LesserThanOrEqual: 7, Like: 8, NotLike: 9, IsNull: 10, IsNotNull: 11, In: 12, NotIn: 13 }
    Zen.Data.QueryModel.CriteriaOperators.registerEnum('Zen.Data.QueryModel.CriteriaOperators', true);
}
if (typeof (Zen.Data.QueryModel.QueryTypes) === 'undefined') {
    Zen.Data.QueryModel.QueryTypes = function () { throw Error.invalidOperation(); }
    Zen.Data.QueryModel.QueryTypes.prototype = { Criteria: 0, Hql: 1, Sql: 2 }
    Zen.Data.QueryModel.QueryTypes.registerEnum('Zen.Data.QueryModel.QueryTypes', true);
}
if (typeof (Zen.Data.QueryModel.OrderDirections) === 'undefined') {
    Zen.Data.QueryModel.OrderDirections = function () { throw Error.invalidOperation(); }
    Zen.Data.QueryModel.OrderDirections.prototype = { Ascending: 0, Descending: 1 }
    Zen.Data.QueryModel.OrderDirections.registerEnum('Zen.Data.QueryModel.OrderDirections', true);
}