function PatientViewModel() {
    this.MRN = ko.observable("");
    this.SSN = ko.observable("");
    this.FirstName = ko.observable("");
    this.MiddleName = ko.observable("");
    this.LastName = ko.observable("");
}

ko.applyBindings(new PatientViewModel());
//app.addViewModel({
//    name: "Patient",
//    bindingMemberName: "home",
//    factory: PatientViewModel
//});
