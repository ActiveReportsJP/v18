import { arWebDesigner } from './web-designer.js';
import { createViewer } from './jsViewer.min.js';

let viewer = null;
let serverUrl = getServerUrl();

function getServerUrl() {
    let baseUrl = 'api';
    let virtualDirName = document.getElementById('virtualDir');

    if (virtualDirName && virtualDirName.href != window.location.origin + '/') {
        return virtualDirName.href + baseUrl;
    }

    return baseUrl;
}

arWebDesigner.create('#ar-web-designer', {
    server: {
        url: serverUrl
    },
	language: 'ja',
    rpx : {enabled:false},
    rdlx: {
        toolBoxContent: {
            cpl:['textbox'],
            fpl:['textbox']
        },
        msl: { enabled:false },
        dashboard: { enabled:false },
        reportParts: {
            enabled: true,
            libraries: [{
                name: 'Treadstone',
                path: 'libraries/Treadstone.rdlx'
            }
            ],
        }
    },
    menu : {
        groupEditor:{visible:false},
        layerEditor:{visible:false},
        toolBox:{visible:true},
    },
    appBar: {
        openButton: { visible: true },
        parametersTab: { visible: false },
        scriptTab:{visible:false},
        contextActionsTab:{visible:false},
    },
    data: {
        dataTab: { visible: false },
        dataSources: { visible:false, shared : {enabled:false} },
        dataSets: { visible:false },
    },
    styles:{
        stylesheet:{canModify:false},
        stylesTab:{visible:false},
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
}).then(designer => {
    designer.app.panels.menu.open("libraries-list");
    designer.app.panels.menu.pin();
});