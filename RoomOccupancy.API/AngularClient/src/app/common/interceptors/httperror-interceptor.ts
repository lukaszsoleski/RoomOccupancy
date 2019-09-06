import { element } from 'protractor';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpResponse,
  HttpErrorResponse,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';
export class HttpErrorInterceptor implements HttpInterceptor {

    /**
     *
     */
    constructor(private toastr: ToastrService) {

    }

    intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((errorResponse: HttpErrorResponse) => {
        console.log(errorResponse);
        let errorMessage = '';
        const status = errorResponse.status;
        const serverErrorCodes = [400, 404, 500];
        let header = 'I need a coffee break.. 😵';
        // client-side error
        if (errorResponse.error instanceof ErrorEvent) {
          errorMessage = `😵 Upps unexpected error: ${errorResponse.error.message}`;
        } else if (serverErrorCodes.indexOf(status) >= 0 ) {
        // server-side error
          header = errorResponse.error.message;
          const errors = errorResponse.error.errors;
          if (errors && errors.length > 0) {
              errorMessage = errors.join(' ');
          }
        } else if (status === 0) {
            errorMessage = 'No connection to the server 😨';
        } else if(status === 401) {
          errorMessage = 'Brak dostępu [401] 😨';
        }
        else if(status === 404){
          errorMessage = 'Strona nie istnieje. 😵';
        }
        else {
            errorMessage = `${errorResponse.message} ${errorResponse.error.error.message}`;
        }

        this.toastr.error(errorMessage, header);

        return throwError(errorMessage);
      })
    );
  }
}
