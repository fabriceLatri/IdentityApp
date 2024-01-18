import { EnvironmentProviders, Provider } from '@angular/core';
import { LocalStorageCacheAdapter } from '@infrastructure/adapters/cache/implementations/cache-impl.adapter';
import { ICachePortToken } from '@presentation/shared/injectionTokens';

export class ProvidersConfiguration {
  public static configure(): (Provider | EnvironmentProviders)[] | undefined {
    return [
      {
        provide: ICachePortToken,
        useClass: LocalStorageCacheAdapter,
      },
    ];
  }
}
