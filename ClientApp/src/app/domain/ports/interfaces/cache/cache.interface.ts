export interface ICachePort {
  getCache(key: string): string | never;
  setCache(key: string, value: string): void;
  deleteCache(key: string): boolean;
  clearCache(): boolean;
}
