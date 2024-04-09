import {exportReport} from './jsViewer.min.js';

let reports = {}

const unique = (function () {
    let cntr = 0;
    return function (prefix) {
        prefix = prefix || "";
        return prefix + cntr++;
    }
})();

function addToLeftList(reportName) {
    addReportToList(reportName, "reportsList", (report) => {
        if (isPrintRunning()) return;
        let spans = report.getElementsByTagName("span");
        addToRightList(spans[0].innerText);
    })
}

function addToRightList(reportName) {
    addReportToList(reportName, "selectedReportsList", (report) => {
        if (!isPrintRunning()) removeFromRightList(report)
    });
    document.getElementById("selected-list-footer").style.display = "";
}

function removeFromRightList(report) {
    let childCount = removeReportFromList(report, "selectedReportsList");
    if (childCount == 0)
        document.getElementById("selected-list-footer").style.display = "none";
}

function addReportToList(reportName, listId, onClick) {
    let reportsList = document.getElementById(listId);

    const report = document.createElement('li');
    report.className = 'reportList_item';
    report.id = unique("id");
    const title = document.createElement('span');
    title.innerText = reportName;
    report.appendChild(title);
    reportsList.appendChild(report);
    report.addEventListener('click', () => onClick(report));
}

function removeReportFromList(report, listId) {
    let reportsList = document.getElementById(listId);
    reportsList.removeChild(report);
    return reportsList.childElementCount;
}

function printNextReport(reportId) {
    let report = document.getElementById(reportId)
    removeFromRightList(report);
    printReport(getFirstReportId());
}

function printReport(reportId) {
    if (reportId == null) {
        document.getElementById("print-button").disabled = false;
        return;
    }

    const report = document.getElementById(reportId);
    const reportName = report.getElementsByTagName("span")[0].innerText;

    let exportSettings = { OnlyForPrint: 'True', ContentInline: 'true', Title: reportName, silentPrint: true }
    let parameters = [];
    const reportInfo = reports[reportName];
    if (reportInfo && reportInfo.settings)
        exportSettings = { ...exportSettings, ...reportInfo.settings }
    if (reportInfo && reportInfo.parameters)
        parameters = reportInfo.parameters;

    exportReport({
        reportID: reportName,
        exportType: 'Pdf',
        settings: exportSettings,
        saveAsDialog: false,
        callback: (url) => silentPrint(url, reportId),
        reportParameters: parameters
    })
}

async function silentPrint(url, reportId) {
    fetch(url).then(async response => {
        if (response.ok) {
            let formData = new FormData()
            formData.append("file", await response.blob());
            fetch("http://localhost:5000/print", {
                method: 'POST',
                mode: 'cors',
                body: formData
            }).then((response) => {
                if (response.ok) {
                    printNextReport(reportId);
                } else {
                    document.getElementById("print-button").disabled = false;
                    response.json().then((json) => alert(json.detail))
                }
            }).catch((err) => {
                document.getElementById("print-button").disabled = false;
                alert(err)
            });
        }
    })
}

function isPrintRunning() { return document.getElementById("print-button").disabled === true }

function getFirstReportId() {
    let lis = document.getElementById("selectedReportsList").getElementsByTagName("li");
    return (lis.length > 0) ? lis[0].id : null;
}

function populateReports() {
    fetch("reports.json")
        .then(response => response.json())
        .then(json => {
            reports = json
            for (let reportName in json) {
                addToLeftList(reportName);
            }
        });
}

const printButton = document.getElementById('print-button');

printButton.addEventListener('click', () => {
    printButton.disabled = true;
    printReport(getFirstReportId());
});

populateReports();