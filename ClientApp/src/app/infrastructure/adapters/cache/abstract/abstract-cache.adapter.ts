import { ICachePort } from '@domain/ports/interfaces';

export abstract class AbstractCacheAdapter<C extends Storage>
  implements ICachePort
{
  abstract storage: C;
  abstract getCache(key: string): string | never;
  abstract setCache(key: string, value: string): void;
  abstract deleteCache(key: string): boolean;
  abstract clearCache(): boolean;
}
