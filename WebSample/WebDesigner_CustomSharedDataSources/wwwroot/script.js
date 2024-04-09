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
    appBar: { openButton: { visible: true } },
    data: { dataSets: { canModify: true }, dataSources: { canModify: true, shared: { enabled: true }} },
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
});