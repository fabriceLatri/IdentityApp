import { ClassConstructor, plainToInstance } from 'class-transformer';

export class BaseAdapter {
  mapTo<T, S>(target: ClassConstructor<T>, src: S): T {
    return plainToInstance(target, src, { excludeExtraneousValues: true });
  }
}
