import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpResponse,
  HttpHandler,
  HttpEvent,
  HttpErrorResponse
} from '@angular/common/http'
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { NGXLogger } from 'ngx-logger';
import { ToastrService } from 'ngx-toastr';
import { environment } from '../../../environments/environment';
import { LoginService } from '../../authentication/login/login.service';
import { ConfirmationDialogService } from '../confirmation-dialog/confirmation-dialog.service';
import { RequestMethod } from '@angular/http';

@Injectable()
export class InterceptorHTTP implements HttpInterceptor {
  constructor(private confirmationDialogService: ConfirmationDialogService,
    private loginService: LoginService,
    private toastr: ToastrService,
    private logger: NGXLogger) { }

  // intercept request and add token
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // modify request
    let that = this;

    if ((request.method == "POST" || request.method == "PUT") && request.body && request.body.hasOwnProperty("RequestValidation")) {
      var body = request.body;
      if (body.RequestValidation) {
        var xsrfToken = body.RequestValidation;
        request = request.clone({
          setHeaders: {
            __RequestValidation: `${xsrfToken}`
          },
        });
        
      }
    }

    const token = that._buildAuthHeader();
    if (token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        },
        url: environment.dataApiUrl + request.url
      });
    }
    else if (request.url.indexOf('https') < 0){
      request = request.clone({
        url: environment.dataApiUrl + request.url
      });
    }
    if (!request.headers.has('Content-Type')) {
      request = request.clone({ headers: request.headers.set('Content-Type', 'application/json') });
    }
    request = request.clone({ headers: request.headers.set('Accept', 'application/json') });

    return next.handle(request).pipe(
      tap((event: HttpEvent<any>) => {
      },
        (error: any) => {
          switch (error.status) {
            case 0: {
              that.toastr.warning('Check your internet connection!',
                'Error!');
              break;
            }
            case 401: {//Authorization Check
              that.loginService.logout();
              break;
            }
            case 400: {//Validation Check
              if (error.error && error.error.ModelState) {
                that.toastr
                  .warning(that.generateValidationMessage(error.error.ModelState),
                    'Validation Error!',
                    {
                      positionClass: 'toast-top-full-width',
                      disableTimeOut: true,
                      closeButton: true
                    });
              }
              else {
                that.logError(error);
              }
              break;
            }
            case 502: {//Request Validation Check
              if (error) {
                that.toastr
                  .warning(error.error.Message,
                    'Validation Error!',
                    {
                      positionClass: 'toast-top-full-width',
                      disableTimeOut: true,
                      enableHtml: true,
                      closeButton: true
                    });
              }
              else {
                that.logError(error);
              }
              break;
            }
            case 403: {
              that.toastr.warning('Service unavailable!',
                    'Error!');
              break;
            }
            case 500: {
              if (error.error && error.error.ExceptionType === 'System.InvalidOperationException') {
                that.toastr
                  .warning(error.error.ExceptionMessage,
                    'Validation Error!',
                    {
                      positionClass: 'toast-top-full-width',
                      disableTimeOut: true,
                      enableHtml: true,
                      closeButton: true
                    });
              }
              else {
                that.logError(error);
              }
              break;
            }
            case 503: {
              that.toastr.warning('Service unavailable!',
                'Error!');
              break;
            }
            default: {
              that.logError(error);
              break;
            }
          }
        }));
  }

  private logError(error: any) {
    let that = this;
    //Error Logging
    let data: any = {};
    if (error instanceof HttpErrorResponse) {
      data = {
        reason: '<p>There was an HTTP error.</p><p>Message: ' + error.message + '</p>',
        status: (<HttpErrorResponse>error).status
      };
      if (error.error && error.error.MessageDetail) {
        data.reason = data.reason + '<p>Message Detail: ' + error.error.MessageDetail + '</p>';
      }
      if (error.error && error.error.ExceptionMessage) {
        data.reason = data.reason + '<p>Exception Message: ' + error.error.ExceptionMessage + '</p>';
      }
      if (error.error && error.error.ExceptionType) {
        data.reason = data.reason + '<p>Exception Type: ' + error.error.ExceptionType + '</p>';
      }
      if (error.error && error.error.StackTrace) {
        data.reason = data.reason + '<p>Stack Trace: ' + error.error.StackTrace + '</p>';
      }
    }
    else if (error instanceof TypeError) {
      data = {
        reason: '<p>Message: ' + error.message + '</p>',
        status: 'There was a Type error.'
      };
    }
    else if (error instanceof Error) {
      data = {
        reason: '<p>Message:' + error.message + '</p>',
        status: 'There was a general error.'
      };
    }
    else {
      data = {
        reason: '<p>Message:' + error.message + '</p>',
        status: 'Nobody threw an error but something happened!'
      };
    }
    that.logger.error(data.reason);
  }

  private generateValidationMessage(data: any): any {
    let message = [];
    Object.values(data).forEach(function (messages) {
      message.push(messages[0]);
    });
    return message;
  }

  private _buildAuthHeader(): string {
    var currentUser = JSON.parse(localStorage.getItem('currentUser'));
    return currentUser && currentUser.token;
  }
}
