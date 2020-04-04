import { Injectable } from '@angular/core';
import { CoreModule } from './core.module';
import { UserManager, User } from 'oidc-client';
import { Constants } from './Constants';
import { Subject } from 'rxjs';

@Injectable({providedIn: CoreModule})
export class AuthService {
    
    private _userManager: UserManager;
    private _user: User;
    private _loginChangedSubject = new Subject<boolean>();

    loginChanged = this._loginChangedSubject.asObservable();

    constructor() {
        const stsSettings = {
            authority: Constants.stsAuthority,
            client_id: Constants.clientId,
            redirect_uri: `${Constants.clientRoot}signin-callback`,
            scope: 'openid profile BankingService TranferLogService IdentityService',
            response_type: 'code',
            post_logout_redirect_uri: `${Constants.clientRoot}signout-callback`
        };

        this._userManager = new UserManager(stsSettings);
    }

    login() {
        return this._userManager.signinRedirect();
    }

    isLoggedIn(): Promise<boolean> {
        return this._userManager.getUser().then(user => {
            const userCurrent = !!user && !user.expired;
            
            if (this._user !== user) {
                this._loginChangedSubject.next(userCurrent);
            }

            this._user = user;
            return userCurrent;
        });
    }

    completeLogin() {
        return this._userManager.signinRedirectCallback().then(user => {
            this._user = user;
            this._loginChangedSubject.next(!!user && !user.expired);
            return user;
        });
    }

    logout() {
        this._userManager.signoutRedirect();
    }

    completeLogout() {
        this._user = null;
        return this._userManager.signoutCallback();
    }
}
