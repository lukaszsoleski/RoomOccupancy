import { element } from 'protractor';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpResponse,
  HttpErrorResponse,
} from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { retry, catchError } from "rxjs/operators";
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
      retry(1),
      catchError((errorResponse: HttpErrorResponse) => {
        console.log(errorResponse);
        let errorMessage = "";
        let status = errorResponse.status;
        let serverErrorCodes = [400,404,500]; 
        let header = "I need a coffee break.. 😵"
        // client-side error
        if (errorResponse.error instanceof ErrorEvent) {
          errorMessage = `😵 Upps unexpected error: ${errorResponse.error.message}`;
        } else if(serverErrorCodes.indexOf(status) >= 0 ){
        // server-side error
          header = errorResponse.error.message
          let errors = errorResponse.error.errors;
          if(errors && errors.length > 0){
              errorMessage = errors.join(' '); 
          }
        }// no connection to server
        else if(status == 0)
        {
            errorMessage = "No connection to the server 😨";
        }// whatever else, just default message
        else{
            errorMessage = errorResponse.error;
        }
       
        this.toastr.error(errorMessage,header);
        
        return throwError(errorMessage);
      })
    );
  }
}
