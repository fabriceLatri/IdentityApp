import { ClassConstructor, plainToInstance } from 'class-transformer';

export class BaseAdapter {
  mapTo<T>(target: ClassConstructor<T>, src: unknown): T {
    return plainToInstance(target, src);
  }
}
