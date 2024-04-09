import {createViewer} from './jsViewer.min.js';

async function silentPrint(url) {
    fetch(url).then(async response => {
        if (response.ok) {
            let formData = new FormData()
            formData.append("file", await response.blob());
            fetch("http://localhost:5000/print", {
                method: 'POST',
                mode: 'cors',
                body: formData
            }).then((response) => {
                if (!response.ok) {
                    response.json().then((json) => alert(json.detail))
                }
            }).catch((err) => alert(err))
        }
    })
}

function selectReportElement(reportName) {
    const reportsList = document.getElementById("reportsList");
    const reports = reportsList.children;

    for (let i = 0; i < reports.length; i++) {
        if (reports[i].children[0].innerText === reportName)
            reports[i].classList.add('active');
        else
            reports[i].classList.remove('active');
    }
}

function getReports() {
    return fetch("reports")
        .then(response => response.json())
        .catch(error => {
            console.error("Error fetching reports:", error);
            return [];
        });
}

function fillReportsList(reports) {
    const reportsList = document.getElementById("reportsList");
    reportsList.innerHTML = "";
    for (let i = 0; i < reports.length; i++) {
        const reportName = reports[i];
        const reportElement = document.createElement('li');
        reportElement.className = 'navbar-item';
        const title = document.createElement('span');
        title.innerText = reportName;
        reportElement.appendChild(title);
        reportsList.appendChild(reportElement);

        reportElement.addEventListener('click', function () {
            openReport(reportName);
        });
    }
}

function openReport(reportName) {
    viewer.openReport(reportName);
    selectReportElement(reportName);
}

const viewer = createViewer({
    element: '#viewerContainer'
});

const silentPrintButton = {
    action: function (item) {
        viewer.export('Pdf', (url) => silentPrint(url), false, {silentPrint:true})
    },
};

viewer.toolbar.desktop.updateItem('$print', silentPrintButton);

getReports().then(reports => {
    if (reports.length > 0) {
        fillReportsList(reports);
        openReport(reports[0]);
    }
});
