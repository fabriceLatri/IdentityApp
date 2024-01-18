import { AbstractCacheAdapter } from '@infrastructure/adapters/cache';
import { LocalStorage } from '@infrastructure/adapters/cache/types';
import { KeyNotFoundStorageError } from '@infrastructure/adapters/cache/errors';
import { Injectable } from '@angular/core';

@Injectable()
export class LocalStorageCacheAdapter extends AbstractCacheAdapter<LocalStorage> {
  //#region Override Fields
  override storage: Storage;
  //#endregion

  //#region Ctor, Dtor
  constructor() {
    super();
    this.storage = localStorage;
  }
  //#endregion

  //#region Override Methods
  override getCache(key: string): string | never {
    const value = this.storage.getItem(key);

    if (!value)
      throw new KeyNotFoundStorageError(
        `Not found value in localStorage with the key: ${key}`
      );

    return value;
  }
  override setCache(key: string, value: string): void {
    this.storage.setItem(key, value);
  }
  override deleteCache(key: string): boolean {
    throw new Error('Method not implemented.');
  }
  override clearCache(): boolean {
    throw new Error('Method not implemented.');
  }
  //#endregion
}
