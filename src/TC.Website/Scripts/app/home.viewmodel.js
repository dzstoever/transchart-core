function HomeViewModel(app, dataModel) {
    var self = this;

    // User Data
    self.myHometown = ko.observable("");

    // User Behaviours
    // call the API to get user info
    Sammy(function () {
        this.get("#home", function () {
            // Make a call to the protected Web API by passing in a Bearer Authorization Header
            $.ajax({
                method: "get",
                url: app.dataModel.userInfoUrl,
                contentType: "application/json; charset=utf-8",
                headers: {
                    'Authorization': "Bearer " + app.dataModel.getAccessToken()
                },
                success: function (data) {
                    self.myHometown("Sammy from the home.viewmodel via the MeApi(authorized with bearer token) says -> Your hometown is " + data.hometown+".");
                }
            });
        });
        this.get("/", function () { this.app.runRoute("get", "#home") });
    });


    // Patient Data
    self.patients = ko.observableArray([]);
    self.selectedPatienTId = ko.observable("");
    self.searchMRN = ko.observable("");
    self.searchSSN = ko.observable("");
    this.searchFirstName = ko.observable("");
    this.searchMiddleName = ko.observable("");
    this.searchLastName = ko.observable("");

    //Patient Operations
    //self.createPatient = function () {
    //    self.patients.push(new Person({ title: this.newTaskText() }));
    //    self.newTaskText("");
    //};
    //self.updatePatient = function (patient) { self.patients.remove(patient) };
    //self.deletePatient = function (patient) { self.patients.remove(patient) };
    
    self.goToPatient = function (patient) { self.selectedPatienTId(patient); };

    // Load initial state from server, convert it to patient instances, then populate self.tasks
    //$.getJSON("/PersonFacadeSvc", function (allData) {
    //    var mappedTasks = $.map(allData, function (item) { return new Patient(item) });
    //    self.patients(mappedTasks);
    //});

    return self;
}

//? = ko.applyBindings()
app.addViewModel({
    name: "Home",
    bindingMemberName: "home",
    factory: HomeViewModel
});
