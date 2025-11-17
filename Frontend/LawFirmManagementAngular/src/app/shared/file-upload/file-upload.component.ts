import { Component, EventEmitter, Input, Output, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { NzUploadChangeParam, NzUploadFile, NzUploadModule } from 'ng-zorro-antd/upload';
import { SharedModule } from '../shared-module';
import { NzFormItemComponent } from "ng-zorro-antd/form";
import { NzColDirective } from "ng-zorro-antd/grid";

@Component({
  standalone: true,
  selector: 'upload-Document',
  imports: [SharedModule, NzFormItemComponent, NzColDirective, NzUploadModule],
  template: `
    <nz-form-item>
      <nz-form-label>{{lable}}</nz-form-label>
      <nz-form-control>
        <nz-upload
          [nzAction]="actionUrl"
          nzListType="picture-card"
          [nzFileList]="fileList"
          [nzShowButton]="fileList.length < (IsMultiple ? 10 : 1)"
          (nzChange)="handleUploadedUrls($event)"
          [nzMultiple]="IsMultiple"
          [nzBeforeUpload]="beforeUpload">
          <div>
            <i nz-icon nzType="plus"></i>
          </div>
        </nz-upload>
      </nz-form-control>
    </nz-form-item>
  `,
  styles: [
    `
      :host ::ng-deep .upload-list-inline .ant-upload-list-item {
        float: left;
        width: 200px;
        margin-right: 8px;
      }
      :host ::ng-deep .upload-list-inline [class*='-upload-list-rtl'] .ant-upload-list-item {
        float: right;
      }
      :host ::ng-deep .upload-list-inline .ant-upload-animate-enter {
        animation-name: uploadAnimateInlineIn;
      }
      :host ::ng-deep .upload-list-inline .ant-upload-animate-leave {
        animation-name: uploadAnimateInlineOut;
      }
    `
  ],
})
export class UploadPictureComponent implements OnInit, OnChanges {
  @Input() FilesUrl: string[] = [];
  @Input() FileUrl: string = '';
  @Input() lable: string = 'Upload';
  @Input() IsMultiple: boolean = false;
  @Input() uploadFolder: string = 'Clients';
  @Output() fileChanged = new EventEmitter<string>();
  @Output() filesChanged = new EventEmitter<string[]>();
  // Emit the original file object(s) so consumers can attach them to FormData when needed
  @Output() fileObjectChanged = new EventEmitter<NzUploadFile | null>();
  @Output() filesObjectChanged = new EventEmitter<NzUploadFile[]>();

  API_BASE_URL: string = 'https://localhost:44356';
  actionUrl: string = '';

  fileList: NzUploadFile[] = [];

  constructor() {}

  ngOnInit(): void {
    this.updateActionUrl();
    this.fileList = [...this.getDefaultFileList()];
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['uploadFolder'] || changes['FileUrl'] || changes['FilesUrl']) {
      this.updateActionUrl();
      this.fileList = [...this.getDefaultFileList()];
    }
  }

  private updateActionUrl(): void {
    this.actionUrl = `${this.API_BASE_URL}/api/upload/${this.uploadFolder}`;
  }

  handleUploadedUrls($event: NzUploadChangeParam): void {
    console.log('Upload event:', $event);

    if ($event.type === 'success') {
      // تحديث قائمة الملفات
      this.fileList = $event.fileList.filter(file => file.status === 'done');

      if (this.IsMultiple) {
        // وضع متعدد الملفات - دعم استجابة string أو array
        const urls = this.fileList.map(file => {
          const resp = file.response;
          if (Array.isArray(resp) && resp.length > 0) return resp[0];
          if (typeof resp === 'string' && resp.length > 0) return resp;
          return file.url || '';
        }).filter(url => url !== '');
        this.filesChanged.emit(urls);
        // أيضاً أرسل كائنات الملفات الأصلية إن كانت متاحة
        const fileObjs: NzUploadFile[] = this.fileList.map(f => f);
        this.filesObjectChanged.emit(fileObjs);
      } else {
        // وضع ملف واحد - أخذ آخر ملف تم رفعه فقط
        if (this.fileList.length > 0) {
          const lastFile = this.fileList[this.fileList.length - 1];
          const resp = lastFile.response;
          let fileUrl = '';
          if (Array.isArray(resp) && resp.length > 0) fileUrl = resp[0];
          else if (typeof resp === 'string' && resp.length > 0) fileUrl = resp;
          else fileUrl = lastFile.url || '';

          this.fileChanged.emit(fileUrl);
          // أرسل كائن الملف الأصلي أيضاً
          this.fileObjectChanged.emit(lastFile);

          // إذا كان هناك أكثر من ملف، احتفظ بالملف الأخير فقط
          if (this.fileList.length > 1) {
            this.fileList = [lastFile];
          }
        } else {
          this.fileChanged.emit('');
          this.fileObjectChanged.emit(null);
        }
      }

      console.log('Uploaded URLs:', this.fileList.map(file =>
        file.response ? file.response[0] : file.url
      ));
    } else if ($event.type === 'removed') {
      // عند حذف ملف
      this.fileList = $event.fileList;

      if (this.IsMultiple) {
        const urls = this.fileList
          .filter(file => file.status === 'done')
          .map(file => file.response ? file.response[0] : file.url || '');
        this.filesChanged.emit(urls);
        this.filesObjectChanged.emit(this.fileList.map(f => f));
      } else {
        const fileUrl = this.fileList.length > 0 ?
          (this.fileList[0].response ? this.fileList[0].response[0] : this.fileList[0].url || '') :
          '';
        this.fileChanged.emit(fileUrl);
        const fileObj = this.fileList.length > 0 ? this.fileList[0] : null;
        this.fileObjectChanged.emit(fileObj);
      }
    }
  }

  beforeUpload = (file: NzUploadFile): boolean => {
    // التحقق من نوع الملف
    const isImage = file.type?.startsWith('image/');
    const isPdf = file.type === 'application/pdf';

    if (!isImage && !isPdf) {
      console.error('نوع الملف غير مدعوم');
      return false;
    }

    // التحقق من حجم الملف (5MB كحد أقصى)
    const maxSize = 5 * 1024 * 1024;
    if (file.size && file.size > maxSize) {
      console.error('حجم الملف كبير جداً');
      return false;
    }

    return true;
  }

  getDefaultFileList(): NzUploadFile[] {
    const files: NzUploadFile[] = [];

      if (this.IsMultiple && this.FilesUrl.length > 0) {
      this.FilesUrl.forEach((url, index) => {
        if (url) {
          const filename = url.split('/').pop() || `file-${index}`;
          files.push({
            uid: `${-index - 1}`,
            name: filename,
            status: 'done',
              // بعض الـ backends ترجع مسارًا يبدأ بـ /upload/... ولكن الملفات قد تكون متاحة تحت /uploads/.
              // حاول استخدام المسار كما هو، وإلا قم بتحويل '/upload/' إلى '/uploads/' لتحسين احتمال التحميل.
              url: url.startsWith('http') ? url : `${this.API_BASE_URL}${url.startsWith('/upload/') ? url : url}`
          });
        }
      });
    } else if (!this.IsMultiple && this.FileUrl) {
      const filename = this.FileUrl.split('/').pop() || 'file-0';
      files.push({
        uid: '-1',
        name: filename,
        status: 'done',
          url: this.FileUrl.startsWith('http') ? this.FileUrl : `${this.API_BASE_URL}${this.FileUrl.startsWith('/upload/') ? this.FileUrl : this.FileUrl}`
      });
    }

    return files;
  }
}
