import { arWebDesigner } from './web-designer.js';
import { createViewer } from './jsViewer.min.js';

let viewer = null;
let reportsList = null;
let serverUrl = getServerUrl();

function getServerUrl() {
    let baseUrl = 'api';
    let virtualDirName = document.getElementById('virtualDir');

    if (virtualDirName && virtualDirName.href != window.location.origin + '/') {
        return virtualDirName.href + baseUrl;
    }

    return baseUrl;
}

getReports().then(reports => {
    reportsList = reports;
    fillReportsList(reports);
    selectReportElement(reports[0].Id);

    arWebDesigner.create('#ar-web-designer', {
        server: {
            url: serverUrl
        },
		language: 'ja',
        document: { id: reports[0].Id, type: { type: 'report', platform: 'rdlx' }},
        appBar: {
            aboutButton: { visible: false },
            parametersTab : { visible: false },
            contextActionsTab: { visible: false }
        },
        propertyGrid: { mode: 'Basic' },
        fonts : [
            { header: 'Questionable Choice' },
            { label: 'Pretty Font', value: 'Comic Sans MS' },
            { header: '' },
            'Arial',
            'Courier New',
            'Times New Roman'
        ],
        data: {
            dataTab: { visible: false },
        },
        menu: {
            logo: { custom: { type: 'css', class: 'example-icon' } },
            toolBox: { visible: false },
            layerEditor: { visible: false },
        },
        statusBar: {
            propertiesModeButton: { visible: false }
        },
        lockLayout: true,
        filterProperties: (descriptors, item, platform) => {
            return descriptors.filter(d =>
            {
                return d.valuePath !== 'Value'
                    && d.valuePath !== 'Name'
                    && d.valuePath !== 'Style.Format'
                    && d.category !== 'propertyDescriptors:categories.layout'
            });
        },
        documents: {
            fileView: { visible: false },
            handlers: {
                onBeforeSave: (info) => {
                    const report = reportsList.find(r => r.Id === info.name);

                    if (report?.Readonly) {
                        const designer = GrapeCity.ActiveReports.Designer.apiOf('ar-web-designer');
                        designer.notifications.warning("レポートは上書きできません。", "もとのレポートは変更できません。別名で保存してください。");
                        return false;
                    }

                    return true;
                },
                onAfterSave: info => {
                    getReports().then(reports => {
                        if (reportsList.length !== reports.length)
                            fillReportsList(reports);

                        selectReportElement(info.id);
                    });
                },
            }
        },
        preview: {
            openViewer: (options) => {
                if (viewer) {
					viewer.theme = options.theme;
                    viewer.openReport(options.documentInfo.id);
                    return;
                }
                viewer = createViewer({
					locale: 'ja',
                    element: '#' + options.element,
                    reportService: {
                        url: 'api/reporting',
                    },
                    reportID: options.documentInfo.id,
                    settings: {
                        zoomType: 'FitPage',
                    },
					theme: options.theme
                });
            }
        }
    })
});

function getReports() {
    return fetch('reports', { method: 'GET' }).then(r => { return r.json() });
}

function fillReportsList(reports) {
    const reportsList = document.getElementById("reportsList");
    reportsList.innerHTML = "";
    for (let i = 0; i < reports.length; i++) {
        const reportName = reports[i].Id;
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
    const designer = GrapeCity.ActiveReports.Designer.apiOf('ar-web-designer');
    designer.documents.openById(reportName, { platform: 'rdlx', type: 'report', subType: 'cpl' })
        .then((e) => {
            if (viewer) {
                backToDesigner();
            }

            selectReportElement(reportName);
        })
        .catch(() => {
            //handle OnBeforeSave rejections
        });
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

function backToDesigner() {
    let backBtn = document.getElementById("viewer-container-button-back");
    if (backBtn) backBtn.click();
}