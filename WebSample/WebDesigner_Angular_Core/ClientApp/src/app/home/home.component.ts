import { Component, ViewChild, ElementRef, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { arWebDesigner } from '@mescius/activereportsnet-designer-ja';
import { JSViewer, createViewer } from '@mescius/activereportsnet-viewer-ja';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: [
        './home.component.css',
        '../../../node_modules/@mescius/activereportsnet-designer-ja/dist/web-designer.css',
        '../../../node_modules/@mescius/activereportsnet-viewer-ja/dist/jsViewer.min.css'
    ],
	encapsulation: ViewEncapsulation.None, 
})

export class HomeComponent implements OnInit {
    private viewer: JSViewer | null = null;

    constructor(private route: ActivatedRoute) { }

    getServerUrl() {
        let baseUrl = 'api';
        let virtualDirName = document.getElementById('virtualDir') as HTMLBaseElement;

        if (virtualDirName && virtualDirName.href != window.location.origin + '/') {
            return virtualDirName.href + baseUrl;
        }

        return baseUrl;
    }

    ngOnInit() {
        arWebDesigner.create('#ar-web-designer', {
            server: {
                url: this.getServerUrl()
            },
            language: 'ja',
            rpx: { enabled: true },
            appBar: { openButton: { visible: true } },
            data: { dataSets: { visible:true, canModify: true }, dataSources: { canModify: true } },
            preview: {
                openViewer: (options: any) => {
                    if (this.viewer) {
                        this.viewer.theme = options.theme;
                        this.viewer.openReport(options.documentInfo.id);
                        return;
                    }
                    this.viewer = createViewer({
                        locale: 'ja',
                        element: '#' + options.element,
                        reportService: {
                            url: 'api/reporting',
                        },
                        reportID: options.documentInfo.id,
						theme: options.theme
                    });
                }
            }
        });
    }

    ngOnDestroy() {
        this.viewer?.destroy();
        arWebDesigner.destroy('#ar-web-designer');
	}
}